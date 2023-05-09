<template>
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
import GameOverDialog from '@/components/GameOverDialog.vue'
import type { Letter } from '@/scripts/letter'
import GameBoard from '@/components/GameBoard.vue'
import KeyBoard from '@/components/KeyBoard.vue'
import WordsList from '@/components/WordsList.vue'
import { onMounted, onUnmounted, reactive, ref } from 'vue'
import { WordsService } from '@/scripts/wordsService'
import Axios from 'axios'
import { watch } from 'vue'

const game = reactive(new WordleGame())
const overlay = ref(true)
const isGameOver = ref(false)

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
  if (event.key.length === 1) {
    game.currentGuess.push(event.key.toUpperCase())
  } else if (event.key === 'Backspace') {
    game.currentGuess.pop()
  } else if (event.key === 'Enter') {
    checkGuess()
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
