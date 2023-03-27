namespace PlatePal.Repositories
{
    public class FavoritesRepository
    {
        private readonly IDbConnection _db;

        public FavoritesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Favorite CreateFavorite(Favorite favoriteData)
        {
            string sql = @"
            INSERT INTO favorites
            (recipeId, AccountId)
            VALUES
            (@recipeId, @AccountId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, favoriteData);
            favoriteData.Id = id;
            return favoriteData;
        }

        internal List<FavoriteRecipe> GetMyFavoriteRecipes(string accountId)
        {
            string sql = @"
            SELECT
            rec.*,
            fav. *,
            acct.*
            FROM favorites fav
            JOIN accounts acct ON fav.accountId = acct.Id
            JOIN recipes rec ON fav.recipeId = rec.Id
            WHERE accountId = @accountId;
            ";
            List<FavoriteRecipe> favoriteRecipes = _db.Query<FavoriteRecipe, Favorite, Account, FavoriteRecipe>(sql, (favoriteRecipe, favorite, account) =>
            {
                favoriteRecipe.FavoriteId = favorite.Id;
                favoriteRecipe.Creator = account;
                return favoriteRecipe;
            }, new { accountId }).ToList();
            return favoriteRecipes;
        }

        internal Favorite GetOne(object id)
        {
            string sql = @"
            SELECT
            *
            FROM favorites
            WHERE id = @id
            ";
            Favorite favorite = _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
            return favorite;
        }

        internal void RemoveFavorite(int id)
        {
            string sql = @"
            DELETE FROM favorites
            WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}