<template>
    <div class="component">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-success text-center">
                    <h1 class="bg-success text-center" id="recipeDetails">Recipe Details</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                        @click="closeEditor"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-4">
                                <img class="img-fluid" :src="recipe?.img" :alt="recipe?.title">
                            </div>
                            <div class="col-8 ">
                                <h2 class="text-center">{{ recipe?.title }}</h2>
                                <div class="row">
                                    <div class="col-6 p-3">
                                        <div class="bg-success text-light w-100">
                                            <h3 class="text-center"> Instructions <i v-if="recipe?.creatorId == account?.id"
                                                    class="mdi mdi-lead-pencil selectable" title="edit instructions"
                                                    @click="openEditor"></i></h3>
                                        </div>
                                        <p v-if="!editorOpen">{{ recipe?.instructions }}</p>
                                        <form v-else @submit.prevent="editInstructions(recipe?.id)">
                                            <div class="form-floating mb-3">
                                                <textarea required v-model="editable.instructions" class="form-control"
                                                    placeholder="instructions" id="instructions" for="instructions"
                                                    name="instructions" style="height: 200px" maxlength="2500"
                                                    minlength="2">{{ recipe?.instructions }}</textarea>
                                                <label for="floatingTextarea2"></label>
                                            </div>
                                            <button type="submit" class="btn bg-success selectable">Submit</button>
                                        </form>
                                    </div>
                                    <div class="col-6 p-3">
                                        <div>
                                            <div class="bg-success text-light w-100">
                                                <h3 class="text-center"> Ingredients </h3>
                                            </div>
                                            <div v-for="i in ingredient" class="col-12 d-flex mb-4 ">
                                                <div v-if="recipe?.creatorId == account.id" class="text-end"> <i
                                                        class="mdi mdi-close-circle-outline selectable"
                                                        title="remove ingredient" @click="removeIngredient(i.id)"></i>
                                                </div>
                                                <div>{{ i?.quantity }} - </div>
                                                <div> {{ i?.name }}</div>
                                            </div>
                                        </div>
                                        <div>
                                            <form v-if="recipe?.creatorId == account?.id" form
                                                @submit.prevent="addIngredient">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-4">
                                                            <div class="form-floating mb-3 h-75">
                                                                <input required v-model="editable.quantity" type="text"
                                                                    class="form-control" id="cover-image" for="ingredient"
                                                                    placeholder="" maxlength="1000" minlength="1">
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
                <div v-if="recipe?.creatorId == account.id" class="modal-footer">
                    <button class="rounded-pill btn bg-danger selectable box-shadow fs-5 me-3"
                        @click="deleteRecipe(recipe?.id)" data-bs-dismiss="modal" aria-label="Close">
                        Delete Recipe <i class="mdi mdi-close-circle-outline"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { ingredientsService } from '../services/IngredientsService.js';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
    setup() {
        const editable = ref({})
        watchEffect(() => {
            if (AppState.recipe != null) {
                editable.value = { ...AppState.recipe }
            } else {
                editable.value = {}
            }
        })
        return {
            account: computed(() => AppState.account),
            recipe: computed(() => AppState.recipe),
            ingredient: computed(() => AppState.ingredients),
            editorOpen: computed(() => AppState.editorOpen),
            editable,
            async addIngredient() {
                try {
                    const formData = editable.value
                    formData.recipeId = AppState.recipe.id
                    await ingredientsService.addIngredient(formData)
                    editable.value = {}

                } catch (error) {
                    Pop.error(error, '[adding ingredient]')
                }
            },

            openEditor() {
                try {
                    AppState.editorOpen = true
                    editable.value = { ...AppState.recipe }
                } catch (error) {
                    Pop.error(error, '[open editor]')
                }
            },
            closeEditor() {
                try {
                    AppState.editorOpen = false
                } catch (error) {
                    Pop.error(error, '[open editor]')
                }
            },

            async editInstructions(recipeId) {
                try {
                    const formData = editable.value
                    formData.recipeId = recipeId
                    await recipesService.editInstructions(formData)
                    editable.value = {}
                    AppState.editorOpen = false
                } catch (error) {
                    Pop.error
                }
            },
            async deleteRecipe(recipeId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this recipe?'))
                        await recipesService.deleteRecipe(recipeId)
                } catch (error) {
                    Pop.error(error, '[delete recipe]')
                }
            },
            async removeIngredient(ingredientId) {
                try {
                    if (await Pop.confirm('Are you sure you would like to remove this ingredient?'))
                        await ingredientsService.removeIngredient(ingredientId)
                } catch (error) {
                    Pop.error(error, '[remove ingredient]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>