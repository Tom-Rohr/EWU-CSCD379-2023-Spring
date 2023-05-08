<template>
  <v-card-text class="font-weight-bold text-body-1 underline py-0"
    >Player Name: {{ name }}
  </v-card-text>
  <v-card-text class="ml-5 py-0">Average Guesses: {{ gameCount }}</v-card-text>
  <v-card-text class="ml-10 pt-0">Games Played: {{ averageAttempts }}</v-card-text>
</template>

<script setup lang="ts">
import { defineProps } from 'vue'

defineProps({
  name: {
    type: String,
    required: true
  },
  averageAttempts: {
    type: Number,
    required: true
  },
  gameCount: {
    type: Number,
    required: true
  }
})
</script>
