<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-4 d-flex justify-content-center" v-for="recipe in recipes">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue';
import { recipesService } from '../services/RecipesService.js'
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import RecipeCard from '../components/RecipeCard.vue';

export default {
  setup() {
    async function getAllRecipes() {
      try {
        await recipesService.getALlRecipes();
      }
      catch (error) {
        Pop.error(error, "[error getting recipes]");
      }
    }
    onMounted(() => {
      getAllRecipes();
    });
    return {
      recipes: computed(() => AppState.recipes)
    };
  },
  components: { RecipeCard }
}
</script>

<style scoped lang="scss"></style>
