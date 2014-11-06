 
 --select top 10 feed.NewsFeedId , feed.Images, feed.Title  , u.Avatar , u.Name from newsfeeds feed join Notifications n on n.NotificationId = feed.NotificationId
 --join Users u on n.userid = u.userid
 -- where ChurchId in ( select Churchid from Users_Churches where userid = 1)  order by feed.CreateDate desc


 insert into users 
 values ( '656e8ca5b332180e4997a9b05525ea38ddf9e564EditDisable' ,'ios', getdate(),null,'','His name 1 '),
 ( 'decbc2a087d101ef81f444c5262d22497e01f95eEditDisable' ,'ios', getdate(),null,'','His name 2'),
 ( '6267693bda99d535cd36ce383875aac0649ae0b0' ,'ios', getdate(),null,'','His name 3')


 insert into Churches
 values ( 'St. George', '','','','','',0,0)

 insert into Users_Churches 
 values (1,1), (2,1), (3,1)