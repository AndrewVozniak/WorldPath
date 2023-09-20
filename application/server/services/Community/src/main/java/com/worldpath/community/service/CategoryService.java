package com.worldpath.community.service;

import com.worldpath.community.DTO.CategoryDTO;
import com.worldpath.community.action.ConvertToCategoryDTOAction;
import com.worldpath.community.model.Category;
import com.worldpath.community.repositories.CategoryRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class CategoryService {
    private final CategoryRepository categoryRepository;
    private final ConvertToCategoryDTOAction convertToCategoryDTOAction;
    private final ModelMapper modelMapper;

    public CategoryService(CategoryRepository categoryRepository, ModelMapper modelMapper) {
        this.categoryRepository = categoryRepository;
        this.convertToCategoryDTOAction = new ConvertToCategoryDTOAction(modelMapper);
        this.modelMapper = modelMapper;
    }

    public CategoryDTO createCategory(CategoryDTO categoryDTO) {
        Category category = modelMapper.map(categoryDTO, Category.class);
        category.setBackgroundColour(categoryDTO.getBackground_colour());
        category.createCategoryDate();

        Category savedCategory = categoryRepository.save(category);

        return convertToCategoryDTOAction.execute(savedCategory);
    }
}
