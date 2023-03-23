<template>
  <nav class="bg-dark p-3 mb-3 pb-5">
    <div class="text-end">
      <Login />
    </div>
    <router-link class=" text-center" :to="{ name: 'Home' }">
      <div class="">
        <h1 class="text-light text-center">PlatePal <i class="mdi mdi-shaker-outline"></i></h1>
        <div class="text-light fs-4">Your recipe companion for every meal</div>
      </div>
    </router-link>
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
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped></style>
