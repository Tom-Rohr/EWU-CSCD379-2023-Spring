<template>
  <v-card class="py-5">
    <v-card-title class="text-center text-h4 pb-5" style="color: #76ff03">
      The Top 10!
    </v-card-title>
    <v-row v-for="i in 10" :key="i" class="pl-8">
      <v-col cols="12" class="py-0">
        <LeaderboardCard />
      </v-col>
    </v-row>
  </v-card>
</template>

<script setup lang="ts">
import LeaderboardCard from '@/components/LeaderboardCard.vue'
</script>
