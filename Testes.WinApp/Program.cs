﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testes.Infra;
using Testes.Infra.Serializador;

namespace Testes.WinApp
{
    public static class Program
    {
        static ISerializador serializador = new SerializadorJson();

        static DataContext contexto = new DataContext(serializador);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {

            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaPrincipalForm(contexto));

            contexto.GravarDados();

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            contexto.GravarDados();
        }

    }
}
