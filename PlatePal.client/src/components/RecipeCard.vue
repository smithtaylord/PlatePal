<template>
    <div class="component">
        <!-- ONCLICK OPEN MODAL -->
        <div>
            <div class="p-3 mt-4  rounded">
                <div class="position-relative rounded  recipe-box-shadow">
                    <img class="recipe-img rounded img-fluid " :src="recipe?.img" :alt="recipe?.title">
                    <div class="position-absolute bottom-0 start-0 glass-bg p-2 mb-3 ms-5 fs-3 text-center w-75 rounded recipe-box-shadow selectable"
                        @click="setRecipeActive(recipe?.id)" title="click for recipe details" data-bs-toggle="modal"
                        data-bs-target="#recipeDetails">
                        {{ recipe?.title }}
                    </div>
                    <div class="position-absolute top-0 start-5 w-100 d-flex justify-content-bwetwen">
                        <div class="glass-bg rounded recipe-box-shadow text-center ms-3 me-5 mt-3 fs-5 p-1 ">
                            {{
                                recipe?.category }}
                        </div>
                        <i v-if="!isFavorite"
                            class="mdi mdi-heart-outline glass-bg p-1 heart rounded recipe-box-shadow text-center ms-5 me-3 mt-3 fs-5 selectable"
                            title="add favorite recipe" @click="addFavorite(recipe?.id)"></i>
                        <i v-else
                            class="mdi mdi-heart text-danger glass-bg p-1 heart rounded recipe-box-shadow text-center ms-5 me-3 mt-3 fs-5 selectable"
                            title="remove favorite recipe" @click="removeFavorite(recipe?.id)"></i>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { recipesService } from '../services/RecipesService.js';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { ingredientsService } from '../services/IngredientsService.js';
import { favoritesService } from '../services/FavoritesService.js';
import { logger } from '../utils/Logger.js';
import { computed } from 'vue';

export default {
    props: {
        recipe: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        return {
            isFavorite: computed(() => AppState.myFavorites.find(f => f.id == props?.recipe?.id)),
            async setRecipeActive(id) {
                try {
                    // AppState.recipe = null
                    await recipesService.getOneRecipe(id)
                    await ingredientsService.getIngredients(id)
                } catch (error) {
                    Pop.error(error, '[set recipe active]')
                }
            },
            async addFavorite(recipeId) {
                try {

                    await favoritesService.addFavorite(recipeId)
                } catch (error) {
                    Pop.error(error, '[add to favorites]')
                }
            },
            async removeFavorite(recipeId) {
                try {
                    if (await Pop.confirm('Are you sure you want to un-favorite this recipe?'))
                        await favoritesService.removeFavorite(recipeId)
                } catch (error) {
                    Pop.error(error, '[remove from favorites]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.recipe-img {
    height: 40vh;
    width: 40vh;
    object-fit: cover;
}


.recipe-category {
    height: 5vh;
}

.glass-bg {
    color: white;
    text-shadow: 1px 1px 2px black;
    background-color: #404a4e6b;
    width: 100%;
    backdrop-filter: blur(7px)
}

.recipe-box-shadow {
    box-shadow: 0 4px 13px rgba(0, 0, 0, 0.253)
}

.heart {
    width: 10vh;
}
</style>