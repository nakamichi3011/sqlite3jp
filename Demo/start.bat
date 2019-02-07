@echo off
chcp 65001
set DBPATH="C:\Users\Aiueo\Documents\ああああ\hoge.db"

sqlite3jp.exe %DBPATH% .\example.sql

pause

exit