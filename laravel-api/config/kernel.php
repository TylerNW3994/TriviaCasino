<?php 

namespace App\Http;

use Illuminate\Foundation\Http\Kernel as HttpKernel;
use Fruitcake\Cors\HandleCors;

class Kernel extends HttpKernel
{
    protected $middleware = [
        \Fruitcake\Cors\HandleCors::class,
    ];

    protected $middlewareGroups = [
        'api' => [
            HandleCors::class,
        ],
    ];
}
