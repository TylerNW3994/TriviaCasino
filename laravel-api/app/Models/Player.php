<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Player extends Model
{
    protected $table = 'players';
    protected $primaryKey = 'id';
    protected $connection = 'mysql';

    public function user()
    {
        return $this->belongsTo(User::class, 'player_id', 'id');
    }
}
