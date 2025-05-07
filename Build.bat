@echo off

REM Đường dẫn tới thư mục chứa dự án của bạn
set project_dir="DemoPayOs"

REM Di chuyển tới thư mục dự án
cd %project_dir%

REM Lệnh publish với các tùy chọn
dotnet publish -r win-x64 -c Release




REM Dừng để bạn có thể nhìn thấy kết quả trước khi thoát
pause
