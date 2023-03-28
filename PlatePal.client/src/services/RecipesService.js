import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService {
    async getALlRecipes() {
        const res = await api.get('api/recipes')
        logger.log(res.data, '[all recipes]')
        AppState.recipes = res.data.map(r => new Recipe(r))
    }

    async createRecipe(formData) {
        const res = await api.post('api/recipes', formData)
        logger.log(res.data, '[creating recipe]')
        AppState.recipes.push(new Recipe(res.data))
    }
}

export const recipesService = new RecipesService();