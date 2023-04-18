<template>
    <div class="simple-keyboard"></div>
</template>

<script setup lang="ts">

import { ref, onMounted } from 'vue';
import Keyboard from 'simple-keyboard';
import 'simple-keyboard/build/css/index.css';

const emit = defineEmits(["onKeyPress"])
document.addEventListener('keydown', (event) => {
  var name = event.key.toUpperCase();
  onKeyPress(name)
  var code = event.code;
  // Alert the key name and key code on keydown
  // console.log(`Key pressed ${name} \r\n Key code value: ${code}`);
}, false);
//const keyboard = ref(null)

const onKeyPress = (button) => {
    emit("onKeyPress", button);
};

onMounted(() => {
    let keyboard = new Keyboard("simple-keyboard", {
        theme: "hg-theme-default hg-layout-default myTheme",
        physicalKeyboardHighlightPress: true,
        physicalKeyboardHighlightTextColor: "green",
        layout: {
            'default': [
                      'Q W E R T Y U I O P',
                       'A S D F G H J K L',
                '{enter} Z X C V B N M ? {bksp}',
            ],
        },
        onKeyPress: onKeyPress,
    });
})
</script>

<style scoped>
.simple-keyboard.myTheme {
  color: black;
  background: transparent;
}
</style>