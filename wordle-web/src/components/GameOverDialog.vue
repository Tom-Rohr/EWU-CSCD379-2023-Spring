<template>
  <v-dialog persistent>
    <v-card height="auto" width="30vh">
      <v-card-title class="text-center"> {{ title }} </v-card-title>

      <v-card-actions class="justify-center">
        <v-btn @click="emit('new-game')" color="success" variant="outlined"> New Game </v-btn>
        <v-btn @click="$router.push('/')" color="success" variant="outlined"> Home </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup lang="ts">
import { WordleGame, GameState } from '@/scripts/wordleGame'
import { defineProps, onMounted, onUpdated, ref } from 'vue'

const title = ref('')
const props = defineProps({
  game: {
    type: WordleGame,
    required: true
  }
})
const emit = defineEmits(['new-game'])

function setTitle() {
  if (props.game.status === GameState.Won) {
    title.value = `You Won in ${props.game.currentGuessIndex + 1} turns!`
  } else if (props.game.status === GameState.Lost) {
    title.value = 'You Lost!'
  }
}
onUpdated(setTitle)
onMounted(setTitle)
</script>

<style scoped>
.v-btn {
  width: 120px;
  font-size: 1rem;
  margin: 0 5px;
}
</style>
