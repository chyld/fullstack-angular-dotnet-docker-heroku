rm web/db/app.db
rm -rf web/Migrations
dotnet ef migrations add Initial --project web
dotnet ef database update --project web
sqlite3 web/db/app.db ".schema" > web/db/schema.sql
