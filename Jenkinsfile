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
		stage('Restore') {
			steps {
				script {
					// Restoring dependencies
                    //sh "cd ${DOTNET_CLI_HOME} && dotnet restore"
                    sh "dotnet restore"
				}
			}
		}
		
        stage('Test') {
            steps {
                script {
                    // Running tests
                    sh "dotnet test --no-restore --configuration Release"
                }
            }
        }

        stage('Run') {
            steps {
                script {
                    // Publishing the application
                    sh "dotnet run --no-build --configuration Release"
                }
            }
        }
    }

    post {
        success {
            echo 'Test, and Run successful!'
        }
    }
}