rm -rf /data/local/tmp/MultiModeSh/packageInfo
mkdir /data/local/tmp/MultiModeSh/packageInfo
cd /data/local/tmp/MultiModeSh/packageInfo
pm list package -f | sed "/\/system/d" | sed "/\/product/d" | sed "/\/apex/d" | sed "/\/vendor/d" | sed "s/\(package\:\)//g" > package
sed "s/.*.apk\=//g" package > packName
sed "s/apk\=.*/apk/g" package > packPath
rm package
echo running pL
sh /data/local/tmp/MultiModeSh/pL.sh &
echo running pA
sh /data/local/tmp/MultiModeSh/pA.sh
