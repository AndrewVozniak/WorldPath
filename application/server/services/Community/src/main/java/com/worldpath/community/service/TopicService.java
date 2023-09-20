package com.worldpath.community.service;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import com.worldpath.community.DTO.TopicDTO;
import com.worldpath.community.model.Topic;
import com.worldpath.community.repositories.TopicRepository;

@Service
public class TopicService {
    private final TopicRepository topicRepository;
    private final ModelMapper modelMapper;

    public TopicService(TopicRepository topicRepository, ModelMapper modelMapper) {
        this.topicRepository = topicRepository;
        this.modelMapper = modelMapper;
    }

    public TopicDTO createTopic(TopicDTO topicDTO) {
        // Convert TopicDTO to Topic
        Topic topic = modelMapper.map(topicDTO, Topic.class);

        // Save topic to database
        Topic savedTopic = topicRepository.save(topic);

        // Convert saved Topic to TopicDTO
        return modelMapper.map(savedTopic, TopicDTO.class);
    }
}
