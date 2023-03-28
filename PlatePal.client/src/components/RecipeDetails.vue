<template>
    <div class="component">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="recipeDetails">Recipe Details</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-4">
                                <img class="img-fluid" :src="recipe?.img" :alt="recipe?.title">
                            </div>
                            <div class="col-8 ">
                                <h2>{{ recipe?.title }}</h2>
                                <div class="row">
                                    <div class="col-6 p-3">
                                        <div class="bg-success text-light w-100">
                                            <h3 class="text-center"> Instructions</h3>
                                        </div>
                                        <p>{{ recipe?.instructions }}</p>
                                    </div>
                                    <div class="col-6 p-3">
                                        <div>
                                            <div class="bg-success text-light w-100">
                                                <h3 class="text-center"> Ingredients</h3>
                                            </div>
                                            <div v-for="i in ingredient" class="col-12 d-flex mb-4 ">
                                                <div>{{ i?.quantity }} - </div>
                                                <div> {{ i?.name }}</div>
                                            </div>
                                        </div>
                                        <div>
                                            <form form @submit.prevent="addIngredient">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-4">
                                                            <div class="form-floating mb-3 h-75">
                                                                <input required v-model="editable.quantity" type="text"
                                                                    class="form-control" id="cover-image" for="ingredient"
                                                                    placeholder="" maxlength="1000" minlength="2">
                                                                <label for="floatingInput">Quantity</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-8">
                                                            <div class="form-floating mb-3 h-75">
                                                                <input required v-model="editable.name" type="text"
                                                                    class="form-control" id="cover-image" for="ingredient"
                                                                    placeholder="" maxlength="1000" minlength="2">
                                                                <label for="floatingInput">Ingredient</label>
                                                            </div>
                                                        </div>
                                                        <div class="text-end">
                                                            <button type="submit" class="btn bg-success selectable">
                                                                <i class="mdi mdi-plus fs-3"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { ingredientsService } from '../services/IngredientsService.js';

export default {
    setup() {
        const editable = ref({})
        return {
            recipe: computed(() => AppState.recipe),
            ingredient: computed(() => AppState.ingredients),
            editable,
            async addIngredient() {
                const formData = editable.value
                formData.recipeId = AppState.recipe.id
                await ingredientsService.addIngredient(formData)
                editable.value = {}
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>