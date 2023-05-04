<template>
  <v-sheet color="blue" height="170px" class="pt-10 my-5">
    <v-card color="indigo-darken-3" class="ma-5" elevation="5" rounded="xl">
      <v-card-title>Hello World</v-card-title>
    </v-card>
  </v-sheet>

  {{ isDialogOpen }}
  <WeatherDialog v-model="isDialogOpen" :weather="currentWeather!"></WeatherDialog>

  <v-card v-for="item in weatherData" :key="item.date" @click="setCurrentWeather(item)">
    {{ item.date + ' ' + item.summary + ' ' + item.temperatureF }}</v-card
  >
</template>

<script setup lang="ts">
import Axios from 'axios'
import { ref } from 'vue'

const isDialogOpen = ref(false)

interface WeatherData {
  date: string
  temperatureC: number
  temperatureF: number
  summary: string
}
const weatherData = ref<WeatherData[]>()
const currentWeather = ref<WeatherData>()

function setCurrentWeather(weather: WeatherData) {
  currentWeather.value = weather
  isDialogOpen.value = true
}

Axios.get('https://localhost:7187/WeatherForecast')
  .then((response) => {
    console.log(response.data)
    weatherData.value = response.data
  })
  .catch((err) => {
    console.log(err)
  })
</script>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
