import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService {
    async getALlRecipes() {
        const res = await api.get('api/recipes')
        logger.log(res.data, '[all recipes]')
        AppState.recipes = res.data.map(r => new Recipe(r))
        AppState.recipesToBeFiltered = res.data.map(r => new Recipe(r))
    }

    async createRecipe(formData) {
        const res = await api.post('api/recipes', formData)
        logger.log(res.data, '[creating recipe]')
        AppState.recipes.push(new Recipe(res.data))
    }

    async getOneRecipe(id) {
        const res = await api.get('api/recipes/' + id)
        logger.log(res.data, '[one recipe]')
        AppState.recipe = new Recipe(res.data)
    }

    async editInstructions(formData) {
        const res = await api.put('api/recipes/' + formData.recipeId, formData)
        logger.log(res.data, '[edit instructions]')
        AppState.recipe = new Recipe(res.data)
    }

    async deleteRecipe(recipeId) {
        const res = await api.delete('api/recipes/' + recipeId)
        const index = AppState.recipes.findIndex(r => r.id == recipeId)
        logger.log(res.data, '[removed recipe]')
        AppState.recipes.splice(index, 1)
    }
}

export const recipesService = new RecipesService();