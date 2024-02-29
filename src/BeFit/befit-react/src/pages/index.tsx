import Head from "next/head";
import Dashboard from "./dashboard";
import { signIn, useSession } from "next-auth/react";
import { useEffect } from "react";

export default function Home() {
  const {data: session, status} = useSession();

  useEffect(() => {
    if (status === "unauthenticated"){
      signIn("azure-ad-b2c");
    }
  });

  return (
    <>
      <Head>
        <title>BeFit</title>
        <meta name="description" content="Dashboard" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      {
        session && (
          <Dashboard/>
        )
      }

    </>
  );
}
