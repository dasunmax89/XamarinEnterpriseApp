dir=$(dirname "$0")
file=XamarinEnterprise.Xamarin.Android.csproj
login_token="xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"

solution_file_path="${dir}/${file}"

echo "Script Executed At: ${solution_file_path}"

host_url="http://localhost:9000"

dotnet-sonarscanner begin /k:"XamarinEnterprise_xamarin" /d:sonar.verbose=true /d:sonar.host.url="${host_url}" /d:sonar.login="${login_token}"

msbuild ${solution_file_path} /t:rebuild

dotnet-sonarscanner end /d:sonar.login="${login_token}"

