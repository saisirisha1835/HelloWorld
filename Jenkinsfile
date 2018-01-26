#!/user/bin/env groovy

node('master') {
    try {
        stage('Checkout'){
            checkout scm
        }

        stage('Build'){
            bat 'nuget.exe restore HelloWorld.sln'
            bat "\"${tool 'MSBuild-15.0'}\" HelloWorld.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
        }

        stage('Backend Test'){
            // Use Xunit.Runner.Console to excute unit test files and generate report in xml format
            bat "packages\\xunit.runner.console.2.3.1\\tools\\net452\\xunit.console UnitTest\\bin\\Release\\UnitTest.dll -xml TestReport\\report.xml"
            // Use Xunit Plugin to read report
            step([$class: 'XUnitPublisher', testTimeMargin: '3000', thresholdMode: 1, thresholds: [[$class: 'FailedThreshold', failureNewThreshold: '5', failureThreshold: '20', unstableNewThreshold: '5', unstableThreshold: '10'], [$class: 'SkippedThreshold', failureNewThreshold: '5', failureThreshold: '20', unstableNewThreshold: '5', unstableThreshold: '10']], tools: [[$class: 'XUnitDotNetTestType', deleteOutputFiles: true, failIfNotNew: true, pattern: 'TestReport/*.xml', skipNoTestFiles: true, stopProcessingIfError: true]]])
        }

        stage('Archive'){
            archiveArtifacts "HelloWorld/bin/Release/**/*"
        }
    }
    catch(error){
        mail body: "project build error is here: ${env.BUILD_URL}", 
                        subject: 'pipeline test email: fail', 
                        to: 'cxu@acr.org'
        throw error
    }
    finally{
        mail body: 'project build successful', 
                        subject: 'pipeline test email: successful', 
                        to: 'cxu@acr.org'
    }
}