cd /data/local/tmp/MultiModeSh/packageInfo
rm appLabel
for path in `cat packPath`
do
  label=`/data/local/tmp/aapt d badging $path | grep application-label-zh-CN:`
  if [ "$label" != "" ]
  then
    echo $label >> label
  else
    label=`/data/local/tmp/aapt d badging $path | grep application-label-zh:`
    if [ "$label" != "" ]
    then
      echo $label >> label
    else
      label=`/data/local/tmp/aapt d badging $path | grep application-label:`
      if [ "$label" != "" ]
      then
        echo $label >> label
      else
        echo NoLabel >> label
      fi
    fi
  fi
done
sed "s/.*:'//g" label | sed "s/'//g" > appLabel
rm label
echo pL runned
