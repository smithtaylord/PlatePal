<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-6 m-auto bg-light box-shadow d-flex justify-content-around text-dark p-3 move-up rounded">
        <div class="fs-3 selectable">Home</div>
        <div class="fs-3 selectable">My Recipes</div>
        <div class="fs-3 selectable">Favorites</div>

      </div>
    </div>
  </div>
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

<style scoped lang="scss">
.move-up {
  transform: translateY(- 50px);

}
</style>
