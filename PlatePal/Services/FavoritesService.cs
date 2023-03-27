namespace PlatePal.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _repo;

        public FavoritesService(FavoritesRepository repo)
        {
            _repo = repo;
        }

        internal Favorite CreateFavorite(Favorite favoriteData)
        {
            Favorite favorite = _repo.CreateFavorite(favoriteData);
            return favorite;
        }

        internal List<FavoriteRecipe> GetMyFavoriteRecipes(string accountId)
        {
            List<FavoriteRecipe> favoriteRecipes = _repo.GetMyFavoriteRecipes(accountId);
            return favoriteRecipes;
        }

        internal string removeFavorite(int favoriteId, string userId)
        {
            Favorite favorite = _repo.GetOne(favoriteId);
            if (favorite == null) throw new Exception($"no favorite with id: {favoriteId}");
            if (favorite.AccountId != userId) throw new Exception($"You are not the account owner, so you cannot remove this favorite");
            _repo.RemoveFavorite(favoriteId);
            return "your favorite has been removed";

        }
    }
}