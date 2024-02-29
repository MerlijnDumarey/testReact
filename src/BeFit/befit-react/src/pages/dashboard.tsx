import { Box, Button, Grid } from '@mui/material';
import { useRouter } from 'next/navigation';
import React from 'react';

const Dashboard = () => {
    const router = useRouter();

    const onNavigateToTests = () => {
        router.push('./takeTestsPage')
    }

    return (
        <Box>
            <Grid container rowSpacing={2} columnSpacing={2}>
                <Grid item xs={12}>
                    <Button variant='contained' onClick={ onNavigateToTests } fullWidth>Test afnemen</Button>
                </Grid>
                <Grid item xs={6}>
                    <Button variant='contained'fullWidth>Beheer testen</Button>
                </Grid>
                <Grid item xs={6}>
                    <Button variant='contained' fullWidth>Beheer atleten</Button>
                </Grid>
            </Grid>
        </Box>
    )
}

export default Dashboard;