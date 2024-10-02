pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
        HOME = "/tmp"
        DOTNET_NUGET_SIGNATURE_VERIFICATION = false
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }


        stage('Test') {
            steps {
                script {
                    // Running tests
                    sh "dotnet test --no-restore --configuration Test"
                }
            }
        }

        stage('Run') {
            steps {
                script {
                    // Publishing the application
                    sh "dotnet run --no-build --configuration Test"
                }
            }
        }
    }

    post {
        success {
            echo 'Build, test, and publish successful!'
        }
    }
}