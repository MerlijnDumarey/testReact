import * as React from 'react';
import { Category } from '@/models/models';
import { Mocking } from '@/services/mockDataService';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import Checkbox from '@mui/material/Checkbox';

interface CategoriesListProps {
    onSelectedCategoriesChange: (selectedCategories: Category[]) => void;
  }

const CategoriesList: React.FC<CategoriesListProps> = ({onSelectedCategoriesChange}) => {
    const categories = Mocking.getAllCategories();
    const [selectedCategories, setSelectedCategories] = React.useState<Category[]>(categories);
    onSelectedCategoriesChange(selectedCategories);

    const handleCheckboxChange = (category: Category) => {
        const isSelected = selectedCategories.some((sc) => sc.id === category.id);

        if (isSelected) {
            setSelectedCategories(selectedCategories.filter((sc) => sc.id !== category.id));
        }
        else {
            setSelectedCategories([...selectedCategories, category]);
        }

    }
    return(
        <div>
            Filter op categorie
            <List>
            {
                categories.map((category) => (
                <ListItem key={category.id}>
                    <ListItemButton>
                        <ListItemIcon>
                            <Checkbox
                            edge="start"
                            tabIndex={-1}
                            disableRipple
                            checked={selectedCategories.some((selectedCategory) => selectedCategory.id === category.id)}
                            onChange={() => handleCheckboxChange(category)}
                            />
                        </ListItemIcon>
                        <ListItemText primary={category.name}/>
                    </ListItemButton>
                </ListItem>))
            }
            </List>
        </div>
    );
}

export default CategoriesList;