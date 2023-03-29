import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FavoritesService {
    async getMyFavorites() {
        const res = await api.get('account/favorites')
        logger.log(res.data, '[account favorites]')
        AppState.myFavorites = res.data
    }

    async addFavorite(id) {
        const res = await api.post('api/favorites', { recipeId: id })
        logger.log(res.data, '[add favorite]')
        const newFavorite = res.data
        newFavorite.favoriteId = res.data.id
        newFavorite.id = res.data.recipeId
        AppState.myFavorites.push(newFavorite)
    }

    async removeFavorite(recipeId) {
        // Finding the favorite Id from the recipe. 
        const favorite = AppState.myFavorites.find(f => f.id == recipeId)
        const favoriteId = favorite.favoriteId
        const res = await api.delete(`api/favorites/${favoriteId}`)
        const index = AppState.myFavorites.findIndex(f => f.favoriteId == favoriteId)
        logger.log(res.data, '[removed favorite]')
        if (index != -1) {
            AppState.myFavorites.splice(index, 1)
        }
    }
}

export const favoritesService = new FavoritesService();