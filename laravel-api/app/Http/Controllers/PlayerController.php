<?php

namespace App\Http\Controllers;

use App\Models\Player;
use Illuminate\Support\Facades\DB;
use Illuminate\View\View;
use Illuminate\Http\Request;
use Illuminate\Http\Response;

class PlayerController extends Controller
{
    public function createPlayer(Request $request)
    {
        $user = User::find($request->input('user_id'));

        if (!$user) {
            return response()->json(['error' => 'User not found'], 404);
        }

        $player = new Player();
        $player->player_id = $user->id;
        $player->display_name = $request->input('display_name');
        $player->save();

        return response()->json(['success' => true, 'player_id' => $player->id]);
    }

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

    public function show($id)
    {
        $player = Player::with('user')->find($id);

        if (!$player) {
            return response()->json(['error' => 'Player not found'], 404);
        }

        return response()->json([
            'display_name' => $player->display_name,
            'email' => $player->user->email,
            'username' => $player->user->username
        ]);
    }

    public function getUser(Request $request) {
        
        $email = $request->input('email');
        $password = $request->input('password');
        return (['email' => $email, 'password' => $password]);
    }
}
