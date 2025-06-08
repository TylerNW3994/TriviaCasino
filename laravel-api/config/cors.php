<?php

return [
    'paths' => ['api/*', 'sanctum/csrf-cookie'],

    'allowedOrigins' => ['*'],

    'allowedOriginsPatterns' => ['http://localhost:\d+'],

    'allowedHeaders' => ['*'],

    'allowedMethods' => ['*'],

    'exposedHeaders' => ['Content-Type', 'Authorization', 'X-Request-ID'],

    'maxAge' => 0,

    'supportsCredentials' => false,
];
