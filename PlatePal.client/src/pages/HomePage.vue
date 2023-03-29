<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-6 m-auto bg-light box-shadow d-flex justify-content-around text-dark p-3 move-up rounded">
        <div class="fs-3 selectable" @click="changeFilter('')">Home</div>
        <div class="fs-3 selectable" @click="changeFilter('myRecipes')">My Recipes</div>
        <div class="fs-3 selectable" @click="changeFilter('favorites')">Favorites</div>

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
  <div class="container-fluid sticky-bottom">
    <div class="row">
      <div class="col-12">
        <div class="text-end m-3">
          <button class="btn bg-success selectable rounded-circle big-btn mdi mdi-plus-circle-outline box-shadow"
            data-bs-toggle="modal" data-bs-target="#createRecipe"></button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed, ref } from 'vue';
import { recipesService } from '../services/RecipesService.js'
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import RecipeCard from '../components/RecipeCard.vue';

export default {
  setup() {
    const filterCategory = ref('')
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
      // recipes: computed(() => AppState.recipes),
      recipes: computed(() => {
        if (filterCategory.value == '') {
          return AppState.recipes
        }
        if (filterCategory.value == 'myRecipes') {
          return AppState.recipes.filter(r => r.creatorId == AppState.account.id)
        }
        if (filterCategory.value == 'favorites') {
          return AppState.myFavorites
        }
      }),

      changeFilter(type) {
        filterCategory.value = type
      }
    };
  },
  components: { RecipeCard }
}
</script>

<style scoped lang="scss">
.move-up {
  transform: translateY(- 50px);

}

.big-btn {
  height: 10vh;
  width: 10vh;
  font-size: 48pt;
  text-shadow: 1px 1px, 2px rgba(0, 0, 0, 0.5);

}
</style>
