<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-4 d-flex justify-content-center" v-for="r in recipes">
        <div class="p-3 mt-4">
          <img class="recipe-img" :src="r.img" :alt="r.title">
          <div>{{ r.title }}</div>
          <div>{{ r.category }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue';
import { recipesService } from '../services/RecipesService.js'
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';

export default {
  setup() {
    async function getAllRecipes() {
      try {
        await recipesService.getALlRecipes();
      } catch (error) {
        Pop.error(error, "[error getting recipes]")
      }
    }
    onMounted(() => {
      getAllRecipes();
    })
    return {
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
.recipe-img {
  height: 35vh;
  width: 35vh;
  object-fit: cover;
}
</style>
