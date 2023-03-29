<template>
    <div class="component">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createRecipe">Create Recipe</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createRecipe">
                        <div class="row">
                            <div class="col-6">
                                <div class="form-floating mb-3">
                                    <input required v-model="editable.title" type="text" class="form-control" id="name"
                                        for="name" placeholder="name..." maxlength="100" minlength="2">
                                    <label for="floatingInput">Title</label>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-floating mb-3">
                                    <input required v-model="editable.img" type="text" class="form-control" id="cover-image"
                                        for="cover-image" placeholder="cover image..." maxlength="1000" minlength="2">
                                    <label for="floatingInput">Cover Image</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-floating mb-3">
                                    <textarea required v-model="editable.instructions" class="form-control"
                                        placeholder="Leave a comment here" id="event-description" for="recipe-instructions"
                                        style="height: 100px" maxlength="2500" minlength="2"></textarea>
                                    <label for="floatingTextarea2">Recipe Instructions</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="mb-3">
                                    <label for="floatingInput">Category</label>
                                    <select v-model="editable.category" required class="form-select" id="type">
                                        <option selected value="Breakfast">Breakfast</option>
                                        <option value="Lunch">Lunch</option>
                                        <option value="Noodles">Noodles</option>
                                        <option value="Soup">Soup</option>
                                        <option value="Mexican">Mexican</option>
                                        <option value="Italian">Italian</option>
                                        <option value="Cheese">Cheese</option>
                                        <option value="Meat">Meat</option>
                                        <option value="Specialty Coffee">Specialty Coffee</option>
                                        <option value="Vegan">Vegan</option>
                                    </select>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="reset" class="btn bg-secondary selectable" data-bs-dismiss="modal">Close</button>
                            <button type="submit" data-bs-dismiss="modal" class="btn bg-primary selectable">Submit</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue';
import { AppState } from '../AppState.js';
import { recipesService } from '../services/RecipesService.js';
import Pop from '../utils/Pop.js';

export default {
    setup() {
        const editable = ref({})

        return {
            editable,
            async createRecipe() {
                try {
                    const formData = editable.value
                    await recipesService.createRecipe(formData)
                    editable.value = {}
                } catch (error) {
                    Pop.error(error, "[create recipe]")
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>