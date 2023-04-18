<template>
  <div>
    <v-row v-for="word in game.guesses" :key="word.text">
      <v-col v-for="letter in word.letters" :key="letter.char">
        <v-btn :color="letter.color">
          {{ letter.char.toUpperCase() }}
        </v-btn>
      </v-col>
    </v-row>
  </div>

  <v-text-field v-model="guess" class="mt-5" disabled @input="guess = guess.toUpperCase()" label="Guess" variant="solo"></v-text-field>

    <simple-keyboard @onKeyPress="handleInput" />

  <v-btn color="primary" class="mt-5" @click="checkGuess">Submit</v-btn>

  <h3> Your guess: [{{ guess.toUpperCase() }}]</h3>
  <h3> Secret word: [{{ game.secretWord.toUpperCase() }}]</h3>
</template>

<script setup lang="ts">

import { WordleGame } from '@/scripts/wordleGame';
import  SimpleKeyboard  from '@/components/SimpleKeyboard.vue';
import { ref, reactive, onMounted} from 'vue';
import type { Word } from '@/scripts/word';

const guess = ref('')
const guesses = Array<Word>()
const game = reactive(new WordleGame())

const validKeys = [
  "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
  "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
  "U", "V", "W", "X", "Y", "Z", "?", "{bksp}", "BACKSPACE", "{enter}", "ENTER"
]
const handleInput = (key) => {
  console.log(key);  
  if (key == "{enter}" || key == "ENTER") {
    if(guess.value.length === 5){checkGuess()
      return
    }
    else {
      alert("Guess must be 5 letters long")
      return
    }
  }
  else if (key == "{bksp}" || key == "BACKSPACE") {
    guess.value = guess.value.slice(0, -1)
    return
  }
  else if(!validKeys.includes(key) || guess.value.length > 4) {
    return
  }
  guess.value += key;
};

onMounted(() => {
  window.addEventListener("keyup", (e) => {
    e.preventDefault;
    let key = 
      e.keyCode == 13
        ? "{enter}"
        : e.keyCode == 8
        ? "{bksp}"
        : String.fromCharCode(e.keyCode).toUpperCase();

  })
})
console.log(game.secretWord)

function checkGuess() {
  game.submitGuess(guess.value)
  guess.value = ""
}
</script>

