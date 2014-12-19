using System.Linq;
using NewsApp.Model;
using NewsAppModel.Extensions;
using NewsAppModel.Model;
using NHibernate;

namespace NewsApp.Data
{
    public class UserRepository : NHibernateRepository<User>, IUserRepository
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }

        public User Merge(int oldUserId, int newUserId)
        {
            var sql = @"DELETE  FROM Subscriptions_Users
                        WHERE   UserId = :o_uid
                                AND ChurchSubscriptionId IN ( SELECT    ChurchSubscriptionId
                                                              FROM      Subscriptions_Users
                                                              WHERE     UserId = :uid );
                        UPDATE  Subscriptions_Users
                        SET     UserId = :uid
                        WHERE   UserId = :o_uid;  ";



            sql += @"DELETE  FROM UserDevices
                        WHERE   UserId = :o_uid
                                AND UserDeviceId IN ( SELECT    UserDeviceId
                                                      FROM      UserDevices
                                                      WHERE     UserId = :uid );
                        UPDATE  UserDevices
                        SET     UserId = :uid
                        WHERE   UserId = :o_uid;";
            sql += @"DELETE  FROM UserDevices
                        WHERE   UserId = :o_uid
                                AND UserDeviceId IN ( SELECT    UserDeviceId
                                                      FROM      UserDevices
                                                      WHERE     UserId = :uid );
                        UPDATE  UserDevices
                        SET     UserId = :uid
                        WHERE   UserId = :o_uid;";

            sql += @"UPDATE  lu
                        SET     lu.UserId = :uid
                        FROM    LikedNewsFeeds_Users lu
                        WHERE   lu.UserId = :o_uid
                                AND NOT EXISTS ( SELECT NULL
                                                    FROM   LikedNewsFeeds_Users l
                                                    WHERE  l.UserId = lu.UserId
                                                        AND l.NewsFeedId = lu.NewsFeedId );

                        DELETE  FROM LikedNewsFeeds_Users
                        WHERE   UserId = :o_uid ";
            sql += @"UPDATE  lu
                        SET     lu.UserId = :uid
                        FROM    Churches_Users lu
                        WHERE   lu.UserId = :o_uid
                                AND NOT EXISTS ( SELECT NULL
                                                    FROM   Churches_Users l
                                                    WHERE  l.UserId = lu.UserId
                                                        AND l.ChurchId = lu.ChurchId );
                        DELETE  FROM Churches_Users
                        WHERE   UserId = :o_uid";

            session.CreateSQLQuery(sql
                 )
                         .SetParameter("uid", newUserId)
                         .SetParameter("o_uid", oldUserId)
                         .ExecuteUpdate();
            //session.CreateQuery("Update UserNotification Set UserId =:uid where UserId=:o_uid ")
            //             .SetParameter("uid", newUserId)
            //             .SetParameter("o_uid", oldUserId)
            //             .ExecuteUpdate();
            session.CreateQuery("Update Comment Set UserId =:uid where UserId=:o_uid  ")
                                    .SetParameter("uid", newUserId)
                                    .SetParameter("o_uid", oldUserId)
                                    .ExecuteUpdate();
            return this.GetById(newUserId);
        }
    }
}