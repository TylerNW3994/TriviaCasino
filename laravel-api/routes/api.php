<?php

// use App\Http\Controllers\PlayerController;

Route::post('/players', 'App\Http\Controllers\PlayerController@store');
Route::get('/players', 'App\Http\Controllers\PlayerController@index');
Route::get('/players/{id}', 'App\Http\Controllers\PlayerController@show');
Route::put('/players/{id}', 'App\Http\Controllers\PlayerController@update');
Route::get('/player/getUser/{email}', 'App\Http\Controllers\PlayerController@getUser');
Route::delete('/players/{id}', 'App\Http\Controllers\PlayerController@destroy');
Route::post('/players/create', [App\Http\Controllers\PlayerController::class, 'createPlayer']);
