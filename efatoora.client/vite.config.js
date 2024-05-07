import { fileURLToPath, URL } from "node:url";
import basicSsl from '@vitejs/plugin-basic-ssl'
import { defineConfig } from "vite";
import plugin from "@vitejs/plugin-react";
// import fs from "fs";
// import path from "path";
// import child_process from "child_process";
// import { env } from "process";

// const baseFolder =
//   env.APPDATA !== undefined && env.APPDATA !== ""
//     ? `${env.APPDATA}/ASP.NET/https`
//     : `${env.HOME}/.aspnet/https`;

// const certificateName = "efatoora.client";
// const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
// const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

// if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
//   if (
//     0 !==
//     child_process.spawnSync(
//       "dotnet",
//       [
//         "dev-certs",
//         "https",
//         "--export-path",
//         certFilePath,
//         "--format",
//         "Pem",
//         "--no-password",
//       ],
//       { stdio: "inherit" }
//     ).status
//   ) {
//     throw new Error("Could not create certificate.");
//   }
// }

export default defineConfig({
  plugins: [plugin(),basicSsl()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  server: {
    proxy: {
      "^/weatherforecast": {
        target: "http://localhost:5100/",
        secure: false,
        logLevel: "debug",
        changeOrigin: true,
      },
      "^/onboard": {
        target: "http://localhost:5100/",
        secure: false,
        logLevel: "debug",
        changeOrigin: true,
      },
      "^/swagger": {
        target: "http://localhost:5100/",
        secure: false,
        logLevel: "debug",
        changeOrigin: true,
      },
      "^/Dashboard/ViewModel": {
        target: "http://localhost:5100/",
        secure: false,
        logLevel: "debug",
        changeOrigin: true,
      },
      "^/Log/GetInvoiceLogs": {
        target: "http://localhost:5100/",
        secure: false,
        logLevel: "debug",
        changeOrigin: true,
      },
      "^/Invoice": {
        target: "http://localhost:5100/",
        secure: false,
        logLevel: "debug",
        changeOrigin: true,
      },
    },
    port: 5173,
    // https: {
    //   key: fs.readFileSync(keyFilePath),
    //   cert: fs.readFileSync(certFilePath),
    // },
  },
});
