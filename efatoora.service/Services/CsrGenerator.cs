using efatoora.service.Contracts;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Microsoft;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System.Collections;
using System.Text;
using ZatcaCore.Contracts;

namespace efatoora.service.Services;

public class CsrGenerator
{
    public static (string, string, string) Generate(Device deviceDetails, BusinessDetail businessDetail)
    {

        AsymmetricCipherKeyPair keyPair = GenerateECDSAKeyPair();

        string privateKeyPem = ExportKeyToPem(keyPair.Private);
        string publicKeyPem = ExportKeyToPem(keyPair.Public);
        //File.WriteAllText(Path.Combine(KeyFolderHelper.Create(), "privatekey.pem"), privateKeyPem);
        Pkcs10CertificationRequest csr = GenerateCsr(deviceDetails, businessDetail, keyPair);

        string base64Csr = ConvertCsrToBase64(csr);
        string formattedCsr = $"-----BEGIN CERTIFICATE REQUEST-----\n{InsertLineBreaks(base64Csr)}\n-----END CERTIFICATE REQUEST-----";

        //File.WriteAllText(Path.Combine(KeyFolderHelper.Create(), "generatedCsr.csr"), formattedCsr);

        return (Convert.ToBase64String(Encoding.UTF8.GetBytes(formattedCsr)), privateKeyPem, publicKeyPem);
    }
    static string InsertLineBreaks(string input)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < input.Length; i += 64)
        {
            sb.AppendLine(input.Substring(i, Math.Min(64, input.Length - i)));
        }
        return sb.ToString().TrimEnd();
    }

    private static AsymmetricCipherKeyPair GenerateECDSAKeyPair()
    {
        ECKeyPairGenerator keyPairGenerator = new ECKeyPairGenerator();
        KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(new SecureRandom(), 256);
        keyPairGenerator.Init(keyGenerationParameters);
        return keyPairGenerator.GenerateKeyPair();
    }

    private static Pkcs10CertificationRequest GenerateCsr(Device deviceDetails, BusinessDetail businessDetail, AsymmetricCipherKeyPair keyPair)
    {
        DerObjectIdentifier regAddress = new DerObjectIdentifier("2.5.4.26");

        IDictionary subjectAttributes = new Hashtable();
        subjectAttributes.Add(X509Name.C, businessDetail.Country);
        subjectAttributes.Add(X509Name.OU, deviceDetails.Name);
        subjectAttributes.Add(X509Name.O, businessDetail.Name);
        subjectAttributes.Add(X509Name.CN, deviceDetails.UniqueNameOfUnit);

        IDictionary subjectAlternativeNameAttributes = new Hashtable();

        subjectAlternativeNameAttributes.Add(X509Name.Surname, deviceDetails.UniqueIdentifier);
        subjectAlternativeNameAttributes.Add(X509Name.UID, businessDetail.VatNumber);
        subjectAlternativeNameAttributes.Add(X509Name.T, deviceDetails.SupportedInvoiceTypes);
        subjectAlternativeNameAttributes.Add(regAddress, businessDetail.AddressRegistered);
        subjectAlternativeNameAttributes.Add(X509Name.BusinessCategory, businessDetail.Industry);

        var subjectName = new X509Name(new ArrayList(subjectAttributes.Keys), subjectAttributes);
        var subjectAltNames = new X509Name(new ArrayList(subjectAlternativeNameAttributes.Keys), subjectAlternativeNameAttributes);

        var generalNames = new GeneralNames(new[] { new GeneralName(subjectAltNames) });


        var extensionsGenerator = new X509ExtensionsGenerator();

        string customString = deviceDetails.Environment == nameof(ZatcaEnvironment.Production)
            ? "ZATCA-Code-Signing"
            : "PREZATCA-Code-Signing";

        extensionsGenerator.AddExtension(MicrosoftObjectIdentifiers.MicrosoftCertTemplateV1, false, new DisplayText(2, customString));

        extensionsGenerator.AddExtension(X509Extensions.SubjectAlternativeName, false, generalNames);

        var extensions = extensionsGenerator.Generate();


        var signatureFactory = new Asn1SignatureFactory("SHA256WITHECDSA", keyPair.Private);

        var attribute = new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(extensions));

        var requestAttributeSet = new DerSet(attribute);

        var certificateRequest =
            new Pkcs10CertificationRequest(signatureFactory, subjectName, keyPair.Public, requestAttributeSet);


        return certificateRequest;
    }
    private static string ExportKeyToPem(AsymmetricKeyParameter privateKey)
    {
        StringBuilder pemBuilder = new StringBuilder();
        PemWriter pemWriter = new PemWriter(new StringWriter(pemBuilder));
        pemWriter.WriteObject(privateKey);
        pemWriter.Writer.Flush();
        return pemBuilder.ToString();
    }
    private static string ConvertCsrToBase64(Pkcs10CertificationRequest csr)
    {
        byte[] csrBytes = csr.GetEncoded();

        string base64Csr = Convert.ToBase64String(csrBytes);

        return base64Csr;
    }
}