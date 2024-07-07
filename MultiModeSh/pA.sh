cd /data/local/tmp/MultiModeSh/packageInfo
rm activitys
for name in `cat packName`
do
  activity=`dumpsys package $name | grep -B3 "Category: \"android.intent.category.LAUNCHER\"" | grep -v Action: | grep -v Category: | grep -v android.intent.action.MAIN | grep -v mPriority= | sed "s/ filter.*//g"`
  if [ "$activity" != "" ]
  then
    echo $activity >> activity
  else
    echo NoActivity >> activity
  fi
done
sed "s/ --.*//g" activity > activitys
rm activity
echo pA runned
