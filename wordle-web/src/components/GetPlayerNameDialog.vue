<template>
  <v-dialog persistent>
    <v-card height="auto" width="30vh">
      <v-card-title class="text-center"> Enter Player Name </v-card-title>
      <v-text-field v-model="newName" class="px-5" autofocus />

      <v-card-actions class="justify-center">
        <v-btn
          @click="props.setCurrentPlayer(newName), console.log(newName)"
          color="success"
          variant="outlined"
        >
          Submit
        </v-btn>
        <v-btn @click="$router.push('/')" color="success" variant="outlined"> Home </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const props = defineProps<{
  setCurrentPlayer: (playerName: string) => void
}>()

const newName = ref('Guest')
</script>

<style scoped lang="css">
.v-btn {
  width: 120px;
  font-size: 1rem;
  margin: 0 5px;
}
</style>
