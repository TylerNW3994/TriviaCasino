FROM php:8.4-fpm

COPY . /app

# Set working directory to /app
WORKDIR /app

# Install dependencies
RUN apt-get update && apt-get install -y \
    libzip-dev \
    zip \
    libfreetype6-dev \
    libjpeg62-turbo-dev \
    libmcrypt-dev \
    libpng-dev \
    libxml2-dev \
    libcurl4-openssl-dev \
    libssl-dev \
    libpq-dev \
    libsqlite3-dev \
    libonig-dev \
    libxslt1-dev \
    libzip-dev \
    libicu-dev

# Install PHP extensions
RUN docker-php-ext-install \
    pdo_mysql \
    mbstring \
    zip \
    curl \
    xml \
    soap \
    bcmath \
    intl

# Install Composer
RUN curl -sS https://getcomposer.org/installer | php -- --install-dir=/usr/local/bin --filename=composer

# Copy composer.lock and composer.json
COPY composer.lock composer.json ./

# Install dependencies
RUN composer install --no-dev --prefer-dist

# Copy the public directory into the /app/public directory
COPY public /app/public

# Copy application code
# COPY . .

# Expose port 9000 for FPM
EXPOSE 9000

# Run command when container launches
CMD ["php", "artisan", "serve", "--host=0.0.0.0", "--port=8000"]
