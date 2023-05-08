<template>
  <v-card class="justify-center pa-2">
    <v-card-title class="text-center text-h4 pb-5" style="color: #76ff03">
      The Top 10!
    </v-card-title>
    <v-row v-for="player in topTen" :key="player.playerID" class="pl-8">
      <v-col cols="12" class="py-0">
        <LeaderboardCard
          :name="player.name"
          :averageAttempts="player.averageAttempts"
          :gameCount="player.gameCount"
        />
      </v-col>
    </v-row>
  </v-card>
</template>

<script setup lang="ts">
import LeaderboardCard from '@/components/LeaderboardCard.vue'
import Axios from 'axios'
import { ref } from 'vue'

interface Player {
  playerID: number
  name: string
  gameCount: number
  averageAttempts: number
}
const topTen = ref<Player[]>()

populateLeaderboardList()

function populateLeaderboardList(): void {
  Axios.get('leaderboard/gettopten')
    .then((response) => {
      console.log(response.data)
      topTen.value = response.data
      console.log(topTen)
    })
    .catch((error) => {
      console.log(error)
    })
}
</script>
