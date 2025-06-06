<?php

namespace App\Http\Controllers;

use App\Models\Player;
use Illuminate\Support\Facades\DB;
use Illuminate\View\View;

class PlayerController extends Controller
{
    public function index()
    {
        $players = Player::all();
        return response()->json($players);
    }

    public function store(Request $request)
    {
        $player = new Player();
        $player->display_name = $request->input('display_name');
        $player->chips = $request->input('chips');
        $player->save();
        return response()->json($player);
    }

    /**
     * Updates the player's chips based on their ID.
     */
    public function updateChips(Request $request)
    {
        $player = Player::findOrFail($request->input('id'));
        $player->chips = $request->input('chips');
        $player->save();
        return response()->json($player);
    }
}
