package com.worldpath.community.action;

import com.worldpath.community.DTO.CategoryDTO;
import com.worldpath.community.action.interfaces.Action;
import com.worldpath.community.model.Category;
import org.modelmapper.ModelMapper;

public class ConvertToCategoryDTOAction implements Action<Category, CategoryDTO> {
    private final ModelMapper modelMapper;

    public ConvertToCategoryDTOAction(ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

    @Override
    public CategoryDTO execute(Category category) {
        CategoryDTO categoryDTO = modelMapper.map(category, CategoryDTO.class);

        categoryDTO.setBackground_colour(category.getBackgroundColour());
        categoryDTO.setCreated_at(category.getCreatedAt());
        categoryDTO.setUpdated_at(category.getUpdatedAt());

        return categoryDTO;
    }
}
