<template>
  <GetPlayerNameDialog v-model="setUserState" :setCurrentPlayer="setCurrentPlayer" width="auto" />
  <div>
    Hello {{ currentPlayer.name }}. You've played {{ currentPlayer.gameCount }} game. You average
    {{ currentPlayer.averageAttempts }} guesses per game.<br />
    PlayerId: {{ currentPlayer.playerId }}
  </div>
  <GameBoard :game="game" />
  <div>
    <WordsList :game="game" />
  </div>
  <div>
    <KeyBoard @letterClick="addChar" :guessedLetters="game.guessedLetters" :game="game" />
  </div>
  <div>Your guess: [{{ game.currentGuess.text }}]</div>
  <div>Secret word: [{{ game.secretWord.toUpperCase() }}]</div>

  <v-overlay :model-value="overlay" class="align-center justify-center" persistent>
    <v-progress-circular color="primary" indeterminate size="64" />
  </v-overlay>
  <v-row>
    <v-col>
      <v-btn @click="addWord()" style="tonal">Add Word</v-btn>
    </v-col>
  </v-row>
  <GameOverDialog :game="game" v-model="isGameOver" @new-game="clickedNewGame" width="auto" />
</template>

<script setup lang="ts">
import { WordleGame, GameState } from '@/scripts/wordleGame'
import { WordsService } from '@/scripts/wordsService'
import type { Letter } from '@/scripts/letter'
import { Player } from '@/scripts/player'
import GameOverDialog from '@/components/GameOverDialog.vue'
import GetPlayerNameDialog from '@/components/GetPlayerNameDialog.vue'
import GameBoard from '@/components/GameBoard.vue'
import KeyBoard from '@/components/KeyBoard.vue'
import WordsList from '@/components/WordsList.vue'
import { computed, onMounted, onUnmounted, reactive, ref, watch } from 'vue'
import Axios from 'axios'
import { PlayersService } from '@/scripts/playersService'

const game = reactive(new WordleGame())
const overlay = ref(true)
const isGameOver = ref(false)
const setUserState = ref(true)

const playerName = ref('')
const playerId = ref()
const gamesPlayed = ref(0)
const averageAttempts = ref<number>(0)

const currentPlayer = computed(() => {
  return reactive(
    new Player(playerId.value, playerName.value, gamesPlayed.value, averageAttempts.value)
  )
})

const clickedNewGame = () => {
  isGameOver.value = false
  newGame()
}

watch(
  () => game.status,
  () => {
    if (game.status == GameState.Won || game.status === GameState.Lost) {
      isGameOver.value = true
    }
  }
)
watch(
  () => setUserState.value,
  () => {
    if (setUserState.value) {
      setUserState.value = false
    }
  }
)

newGame()

onMounted(async () => {
  window.addEventListener('keyup', keyPress)
})
onUnmounted(() => {
  window.removeEventListener('keyup', keyPress)
})

function addWord() {
  overlay.value = true
  Axios.post('word/AddWordFromBody', {
    text: 'tests',
    isCommon: true
  })
    .then((response) => {
      overlay.value = false
      console.log(response.data)
    })
    .catch((error) => {
      console.log(error)
    })
}

function addPlayer(playerName: string) {
  overlay.value = true
  Axios.post(`leaderboard/?name=${playerName}`, {
    name: playerName,
    gameCount: 0,
    averageAttempts: 0
  })
    .then((response) => {
      overlay.value = false
      console.log(response.data)
      console.log('player successfully added')
    })
    .catch((error) => {
      console.log(error)
    })
}

async function setCurrentPlayer(name: string) {
  if (!(await PlayersService.isExistingPlayer(name))) {
    addPlayer(name)
  } else {
    Axios.get('leaderboard/GetPlayer', {
      params: {
        name: name
      }
    })
      .then((response) => {
        playerName.value = response.data.name
        playerId.value = response.data.playerID
        gamesPlayed.value = response.data.gameCount
        averageAttempts.value = response.data.averageAttempts

        console.log(response.data)
        console.log(currentPlayer.value)
      })
      .catch((error) => {
        console.log(error)
      })
  }

  if (setUserState.value) {
    setUserState.value = false
  }
  console.log(playerName.value + ' has been set as the current user')
}

function newGame() {
  overlay.value = true
  Axios.get('word')
    .then((response) => {
      game.restartGame(response.data)
      console.log(game.secretWord)
      setTimeout(() => {
        overlay.value = false
      }, 600)
    })
    .catch((error) => {
      console.log(error)
      game.restartGame(WordsService.getRandomWord())
      console.log(game.secretWord)
      overlay.value = false
    })
}

function addChar(letter: Letter) {
  game.currentGuess.push(letter.char)
}

function keyPress(event: KeyboardEvent) {
  if (!setUserState.value) {
    if (event.key.length === 1) {
      game.currentGuess.push(event.key.toUpperCase())
    } else if (event.key === 'Backspace') {
      game.currentGuess.pop()
    } else if (event.key === 'Enter') {
      checkGuess()
    }
  }
}

function checkGuess() {
  if (WordsService.isValidWord(game.currentGuess.text.toLowerCase())) {
    game.submitGuess()
  } else {
    alert('Invalid word')
  }
}
</script>
