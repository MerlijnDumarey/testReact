import { Console } from "console";
import NextAuth from "next-auth";
import AzureADB2CProvider from "next-auth/providers/azure-ad-b2c";

export default NextAuth({
    providers: [
        AzureADB2CProvider({
            tenantId: process.env.AZURE_AD_B2C_TENANT_ID,
            clientId: process.env.AZURE_AD_B2C_CLIENT_ID,
            clientSecret: process.env.AZURE_AD_B2C_CLIENT_SECRET,
            primaryUserFlow: process.env.AZURE_AD_B2C_PRIMARY_USER_FLOW,
            authorization: {params: {scope: "https://sportbewegen.onmicrosoft.com/sportbewegenapi/myscope openid offline_access" }},
        }),
    ], callbacks: {
        async jwt({ token, account}) {
            if (account){
                token.accessToken = account.access_token
            } return token
        },

        async session({ session, token, user }) {
            session.accessToken = token.accessToken
            return session
        },
    },
});