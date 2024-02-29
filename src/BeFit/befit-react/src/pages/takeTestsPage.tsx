import Head from "next/head";
import { usePathname } from "next/navigation";
import TestsList from "@/components/TestsList";
import {Category, Test} from "@/models/models";
import { Mocking } from "../services/mockDataService";
import CategoriesList from "@/components/CategoriesList";
import SelectedTestsList from "@/components/SelectedTestsList";
import { signIn, useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import { Grid } from "@mui/material";

export default function Home() {
  const tests = Mocking.getAllTests();
  const {data: session, status} = useSession();
  const [selectedCategories, setSelectedCategories] = useState<Category[]>([]);
  const [selectedTests, setSelectedTests] = useState<Test[]>([]);

  useEffect(() => {
    if (status === "unauthenticated"){
      signIn("azure-ad-b2c");
    }
  });

  const filteredTests = tests.filter((test) => {
      return selectedCategories.some((Category) => test.categoryId.includes(Category.id));
  });

  const handleSelectedTestsChange = (tests: Test[]) => {
    setSelectedTests(tests);
  }
  const pathName = usePathname();
  console.log(pathName);

  const handleSelectedCategoriesChange = (categories: Category[]) => {
    setSelectedCategories(categories);
  };

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
          <Grid container rowSpacing={2} columnSpacing={2}>
            <Grid item xs={6}>
              <Grid item xs={12}>
                <CategoriesList onSelectedCategoriesChange={handleSelectedCategoriesChange}/>
              </Grid>
              <Grid item xs={12}>
                <SelectedTestsList selectedTests={selectedTests} onSelectedTestsChange={handleSelectedTestsChange}></SelectedTestsList>
              </Grid>
            </Grid>
            <Grid item xs={6}>
              <TestsList selectedTests={selectedTests} filteredTests={filteredTests} onSelectedTestsChange={handleSelectedTestsChange}/>
            </Grid>
          </Grid>
        )
      }

    </>
  );
}
