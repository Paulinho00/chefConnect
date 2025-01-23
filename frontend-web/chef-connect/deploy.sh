#!/bin/bash

set -e

BUCKET_NAME="frontend-chef-connect"

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m'

echo -e "${GREEN}Starting deployment process...${NC}"

# Build
echo -e "${GREEN}Building Angular application...${NC}"
ng build
if [ $? -ne 0 ]; then
    echo -e "${RED}Build failed!${NC}"
    exit 1
fi

# Check if dist directory exists
if [ ! -d "dist/chef-connect/browser" ]; then
    echo -e "${RED}Build directory not found! Make sure you're in the right directory and the build was successful.${NC}"
    exit 1
fi

# Clear the S3 bucket
echo -e "${GREEN}Clearing existing files from S3 bucket...${NC}"
aws s3 rm s3://$BUCKET_NAME --recursive
if [ $? -ne 0 ]; then
    echo -e "${RED}Failed to clear S3 bucket!${NC}"
    exit 1
fi

# Upload new files
echo -e "${GREEN}Uploading new files to S3...${NC}"
aws s3 sync dist/chef-connect/browser s3://$BUCKET_NAME --delete
if [ $? -ne 0 ]; then
    echo -e "${RED}Failed to upload files to S3!${NC}"
    exit 1
fi

echo -e "${GREEN}Deployment completed successfully!${NC}"