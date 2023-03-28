import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Ingredient } from "../models/Ingredient.js";

class IngredientsService {
    async getIngredients(id) {
        const res = await api.get(`api/recipes/${id}/ingredients`)
        logger.log(res.data, '[recipe ingredients]')
        AppState.ingredients = res.data.map(i => new Ingredient(i))
    }

    async addIngredient(formData) {
        const res = await api.post('api/ingredients', formData)
        logger.log(res.data, '[adding ingredient]')
        AppState.ingredients.push(new Ingredient(res.data))
    }

}

export const ingredientsService = new IngredientsService()