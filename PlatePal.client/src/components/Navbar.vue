<template>
  <nav class="bg-success bg-gradient p-3 mb-3 pb-5">
    <div class="container-fluid">
      <div class="row">
        <div class="col-9 col-md-3 offset-md-8 d-flex align-items-center">
          <div class="input-group mb-3">
            <input v-model="editable.search" @input="searchRecipes" type="text" class="form-control"
              placeholder="Search..." aria-label="search" aria-describedby="button-addon2">
            <!-- <button class="btn bg-success border-light" type="button" id="button-addon2"> <i
                class="mdi mdi-magnify text-light"></i></button> -->
          </div>
        </div>
        <div class="col-3 col-md-1 text-center">
          <Login />
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <router-link class=" text-center" :to="{ name: 'Home' }">
            <div class="">
              <h1 class="text-light text-center">PlatePal <i class="mdi mdi-shaker-outline"></i></h1>
              <div class="text-light fs-4">Your recipe companion for every meal</div>
            </div>
          </router-link>
        </div>
      </div>

    </div>
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>

        </li>
      </ul> -->
    <!-- LOGIN COMPONENT HERE -->
  </nav>
</template>

<script>

import { ref, computed } from 'vue';
import { AppState } from '../AppState.js';
import Login from './Login.vue'
export default {
  setup() {
    const editable = ref({ search: '' })

    function searchRecipes() {
      setTimeout(() => {
        AppState.recipes = AppState.recipesToBeFiltered.filter(r => r.category.toLowerCase().includes(editable.value.search.toLowerCase()));
      }, 500);
      if (editable.value.search == '') {
        AppState.recipes = AppState.recipesToBeFiltered;
      }
    }
    return {
      editable,
      searchRecipes,
      recipes: computed(() => AppState.recipesToBeFiltered)

    }
  },
  components: { Login }
}
</script>

<style scoped></style>
