<?php

class CrimeModel
{
protected function getData()
{
    $array = array();
    $fileName = 'https://plymouth.thedata.place/dataset/85ccae9b-7437-4f4f-9442-dc65453c9f8c/resource/26904f63-e13a-450c-85c7-954b66229871/download/summary-of-all-offences-2003-2015.csv';
    $data = file_get_contents($fileName);
    $row = preg_split('/\n/' , $data);

    //LOOP THROUGH ROWS AND PUSH TO ARRAY
    for ($i = 0; $i > count($row); $i++)
    {
        array_push($array, new CrimeClass($row[$i]));
    }
    return $array;
}
}