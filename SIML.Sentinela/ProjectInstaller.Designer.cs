namespace SIML.Sentinela
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstallerSIMLSentinela = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstallerSIMLSentinela = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstallerSIMLSentinela
            // 
            this.serviceProcessInstallerSIMLSentinela.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.serviceProcessInstallerSIMLSentinela.Password = null;
            this.serviceProcessInstallerSIMLSentinela.Username = null;
            // 
            // serviceInstallerSIMLSentinela
            // 
            this.serviceInstallerSIMLSentinela.ServiceName = "SIMLSentinela";
            this.serviceInstallerSIMLSentinela.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstallerSIMLSentinela.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerSIMLSentinela,
            this.serviceInstallerSIMLSentinela});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerSIMLSentinela;
        private System.ServiceProcess.ServiceInstaller serviceInstallerSIMLSentinela;
    }
}