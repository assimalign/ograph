<template>
   <div>
      hello
      {{ text }}
   </div>
   <!-- <ograph-editor ref="ographEditor" :viewType="viewType"></ograph-editor> -->
</template>

<script lang="ts">
import { defineComponent, onMounted, onUnmounted, provide, ref } from "vue";
import { VsCode } from "./types";
import OGraphEditor from "./components/OGraphEditor.vue";

declare const vscode: VsCode;

export default defineComponent({
   name: 'App',
   // components: { OGraphEditor },
   setup() {
      // const ographEditor = ref<InstanceType<typeof OGraphEditor>>()
      // const viewType = ref('');
      const text = ref<any>()

      /**
       * Receive and process the content of the message.
       * @param event Message which was sent from the extension.
       */
      // function getDataFromExtension(event: MessageEvent): void {
      //    const message = event.data;
      //    const text = message.text;

      //    switch (message.type) {
      //       case viewType.value + '.updateFromExtension': {
      //          ographEditor.value?.updateContent(text);
      //          break;
      //       }
      //       case viewType.value + '.undo':
      //       case viewType.value + '.redo': {
      //          ographEditor.value?.updateContent(text, true);
      //          break;
      //       }
      //       default: break;
      //    }
      // }

      onMounted(() => {
         const state = vscode.getState();


         text.value = state

         // console.log(state)

         // if (state) {
         //    viewType.value = state.viewType;
         //    ographEditor.value?.updateContent(state.text);
         // }

         // Add event listener for receiving messages from the extension
         window.addEventListener('message', getDataFromExtension);
      })

      onUnmounted(() => {
         window.removeEventListener('message', getDataFromExtension);
      })

      // Publish the VSCodeAPI to all components
      provide('vscode', vscode);

      return {
         text
         // ographEditor,
         // viewType
      }
   }
});
</script>

<style>
#app {
   font-family: Avenir, Helvetica, Arial, sans-serif;
   -webkit-font-smoothing: antialiased;
   -moz-osx-font-smoothing: grayscale;
   text-align: center;
   color: papayawhip;
   margin-top: 20px;
}
</style>